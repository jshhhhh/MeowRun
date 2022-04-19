<script lang="ts">
    import axios from 'axios';
    import API_URL from '../API/url';
    import { storeVoteCounts } from "../store/store";
    import Vote from "../sub/Vote.svelte";

    const oneHour = 1000*60*60 // an hour in millseconds
    let votePeriod = 7 // 7 days
    
    // calculate total votes
    let totalVotes = $storeVoteCounts.garfield + $storeVoteCounts.fishCat + $storeVoteCounts.tricolor

    let voteList = {
        "Garfield" : $storeVoteCounts.garfield,
        "FishCat": $storeVoteCounts.fishCat, 
        "Tricolor": $storeVoteCounts.tricolor
    }

    // decide vote winner
    const revealWinner = () => {
        const mostVoted = Math.max(
            voteList.FishCat, 
            voteList.Garfield, 
            voteList.Tricolor
        )
        const winner = Object.keys(voteList).find((character) => voteList[character] === mostVoted)
        console.log(winner)
        return winner
    }

    // fetch vote data from server and deliver to children components
    // vote should be fetched on a regular basis
    const fetchVotes = async () => {

        if (shouldFetchVotes()){
            votePeriod--; // decrease vote days by one
            const res = await axios.get(API_URL.VOTE.currentVotes)

            if (res.status === 200) {
                // update vote counts in store with the data
                // CHECK: changed data should be rendered automatically
                $storeVoteCounts.fishCat = res.data.fishCat
                $storeVoteCounts.garfield = res.data.garfield
                $storeVoteCounts.tricolor = res.data.tricolor
            }
        }
        console.log("Not fetching time yet")

        isVoteEnded()
    }

    const shouldFetchVotes = () => {
        const fetchTime = new Date()
        fetchTime.setHours(12,0) // set 12pm
        console.log("vote should be updated at: ", fetchTime.getHours(), "pm")

        const currentTime = new Date()
        console.log("current time is: ", currentTime.getHours())
        
        if (currentTime.getHours() === fetchTime.getHours()) return true
        else false
    }

    // 1. check if 12pm 
    const initVote = () => {
        const currentTime = new Date()
        if (currentTime.getHours() === 12) fetchVotes()
    }

    // 2. repeat by one hour 
    const completeVote = setInterval(initVote, oneHour)

    // 3. check vote periods and decide completion
    const isVoteEnded = () => {
        if (votePeriod === 0) clearInterval(completeVote)
        console.log("voting is being processed")
    }
</script>

<main id="characterVote">
    <h1>Vote the coolest characters</h1>
    <h2>Who should be the upcoming character?</h2>
    <div id="votingBooth">
        <Vote 
            characterName='Garfield' 
            imageSource=''
            videoSource="https://sketchfab.com/models/224963c5dbc74c3c8eb89fa100d7251c/embed"
            voteCount={$storeVoteCounts.garfield} />
        <Vote 
            characterName='FishCat' 
            imageSource=''
            videoSource='https://sketchfab.com/models/c79305df250a4d1e9fe0ffca5f232c1d/embed'
            voteCount={$storeVoteCounts.fishCat} />
        <Vote 
            characterName='Tricolor' 
            imageSource=''
            videoSource='https://sketchfab.com/models/eb30bd609a3c422f8cf77d2916b1b559/embed'
            voteCount={$storeVoteCounts.tricolor} />
    </div>

    <div id="voteResult">
        <p>Current Leading: <b>{revealWinner()}</b></p>
        <span>*Votes are updated at 12 pm(KST)</span>
        <p>Total votes: {totalVotes}</p>
    </div>
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #characterVote {
        text-align: center;
    }
    h1 { font-size: $unit *3; }
    #votingBooth {
        @include flexRow();
        justify-content: left;
        align-items: center;
        overflow-x: auto;
        overflow-y: hidden;
        white-space: nowrap;
    }
    #voteResult {
        margin-bottom: 30px;
        p { 
            font-size: $unit *1.5;
            margin-bottom: 0;
        }
    }
</style>