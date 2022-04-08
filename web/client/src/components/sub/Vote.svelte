<script lang="ts">
    // below should be changed based on schema
    let firstVote = 70
    let secondVote = 40
    let thirdVote = 22
    let totalVotes = firstVote + secondVote + thirdVote

    // fetchVotes should update votes
    const fetchVotes = () => {
        // TO DO
    }

    // form submission
    const handleSubmit = () => {
        console.log(document.forms.namedItem("characterVoteForm"))
    }
</script>


<main>
    * Votes are updated at 12 pm
    <h1>Upcoming characters</h1>

    <!-- TO DO: embed 3d models here -->
    <div id="characterDisplay">
        <img class="character" src="/icons/item.gif" alt="">
        <img class="character" src="/icons/item.gif" alt="">
        <img class="character" src="/icons/item.gif" alt="">
    </div>

    <form name="characterVoteForm" id="characterVoteForm" on:submit|preventDefault={handleSubmit}>
        <label for="">Garfield
            <input type="radio" name="character" id="" value="Garfield" />
        </label>
        <label for="">Tricolor
            <input type="radio" name="character" id="" value="Tricolor" />
        </label>
        <label for="">FishCat
            <input type="radio" name="character" id="" value="FishCat" />
        </label>
        <button>Vote</button>
    </form>


    <div id="voteResult">
        <div class="voting">
            character name here
            <label for="vote">{firstVote} votes</label>
            <progress id="vote" max="100" value={firstVote}>{firstVote}</progress>
        </div>
    
        <div class="voting">
            character name here
            <label for="vote">{secondVote} votes</label>
            <progress id="vote" max="100" value={secondVote}>{secondVote}</progress>
        </div>
        
        <div class="voting">
            character name here
            <label for="vote">{thirdVote} votes</label>
            <progress id="vote" max="100" value={thirdVote}>{thirdVote}</progress>
        </div>
    
        <div class="totalVotes">
            total votes: {totalVotes}
        </div>
    </div>
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #characterDisplay {
        @include flexRow();
        .character {
            max-width: 15%;
            border-radius: 50%;
        }
    }
    #characterVoteForm {
        @include flexRow();
    }
</style>