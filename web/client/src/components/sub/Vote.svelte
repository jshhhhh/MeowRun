<script lang="ts">
    import axios from 'axios'
    import API_URL from '../API/url';
    import PlayButton from './PlayButton.svelte';

    // component props
    export let characterName;
    export let imageSource;
    export let videoSource ='';
    export let voteCount;

    let errorMessage = ''

    const TARGET_COUNTS = 500;

    // fetchVotes should update votes
    const fetchVotes = async () => {
        // fetch vote counts from server
        const res = await axios.get(API_URL.VOTE.currentVotes)

        // store thevote count
        voteCount = res.data
    }

    const updateVotes = async () => {
        const res = await axios.put(API_URL.VOTE.updateVotes, {
            voteCount: voteCount+1
        })

        // update current vote count
        if (res.status = 200 || 204 ) voteCount++
        else errorMessage = 'Cannot update votes'
    }

    // form submission
    const handleSubmit = () => {
        console.log(document.forms.namedItem("characterVoteForm"))
    }
</script>


<main id="characterVote">
    <div
        id="characterDisplay" 
        class="sketchfab-embed-wrapper voteContent">
        <iframe 
            title={characterName} 
            loading='lazy'
            frameborder="0" 
            allowfullscreen allow="autoplay; fullscreen; xr-spatial-tracking" 
            xr-spatial-tracking execution-while-out-of-viewport execution-while-not-rendered web-share 
            width="400"
            height="300"
            src={videoSource}> 
        </iframe> 
    </div>

    <form name="characterVoteForm" class="voteContent" id="characterVoteForm" on:submit|preventDefault={handleSubmit}>
        <span id="characterName">{characterName}</span>
        <PlayButton 
            isTransparent={false}
            buttonText='Vote'
            jumpTo='' />     
    </form>

    <div id="currentVotes" class="voteContent">
        <label for="vote">{voteCount} votes out of {TARGET_COUNTS}</label>
        <progress id="vote" max={TARGET_COUNTS} value={voteCount}>{TARGET_COUNTS}</progress>
    </div>

    
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #characterVote {
        padding: $unit*2 $unit*4 $unit*4 $unit*4;

        .voteContent {
            @include flexColumn();
            justify-content: center;
            align-items: center;
            padding-top: $unit;
        }
        #characterDisplay {
            @include flexRow();
            .character {
                max-width: 15%;
                border-radius: 50%;
            }
        }
        #characterVoteForm {
            gap: $unit*0.5;
            #characterName {
                font-size: $unit*1.5;
            }
        }
    }
</style>