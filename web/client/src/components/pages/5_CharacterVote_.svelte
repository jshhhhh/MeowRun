<script lang="ts">

    import Vote from "../sub/Vote.svelte";
    // below should be changed based on schema
    let garfieldVote = 70
    let fishCatVote = 40
    let tricolorVote = 22
    let totalVotes = garfieldVote + fishCatVote + tricolorVote

    let voteList = {
        "Garfield" : garfieldVote,
        "FishCat": fishCatVote, 
        "Tricolor": tricolorVote
    }

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
</script>

<main id="characterVote">
    <h1>Vote the coolest characters</h1>
    <h2>Who should be the upcoming character?</h2>
    <div id="votingBooth">
        <Vote 
            characterName='Garfield' 
            imageSource=''
            videoSource="https://sketchfab.com/models/224963c5dbc74c3c8eb89fa100d7251c/embed"
            voteCount={garfieldVote} />
        <Vote 
            characterName='FishCat' 
            imageSource=''
            videoSource='https://sketchfab.com/models/c79305df250a4d1e9fe0ffca5f232c1d/embed'
            voteCount={fishCatVote} />
        <Vote 
            characterName='Tricolor' 
            imageSource=''
            videoSource='https://sketchfab.com/models/eb30bd609a3c422f8cf77d2916b1b559/embed'
            voteCount={tricolorVote} />
    </div>

    <div id="voteResult">
        <p>Current Leading: <b>{revealWinner()}</b></p>
        <span>*Votes are updated at 12 pm</span>
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