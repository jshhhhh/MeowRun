<script lang="ts">
import Feature from "../sub/Feature.svelte";
import PlayButton from "../sub/PlayButton.svelte";


</script>


<main id="whyMeowRun">
<h1>Why MeowRun</h1>

<div id="features">
    <Feature 
        iconSource="/icons/controll.gif"
        title="Easy to control"
        paragraph="MeowRun is a 3D game with easy controller. Move your character with arrow keys and space. That's it!" />
    <Feature 
        iconSource="/icons/player.gif"
        title="Realistic but cute"
        paragraph="If you are a cat person, You will fall in love with MeowRun characters! They are vivid, friendly, and 
        most importantly, represent you the player in game!" />
    <Feature 
        iconSource="/icons/enemy.gif"
        title="Energetic enemies"
        paragraph="Game without obstacles is not a game. Defeat the enemies and dash to finish line safely." />
    <Feature 
        iconSource="/icons/item.gif"
        title="Various collectibles"
        paragraph="Find hidden items in 3D map - they will power you up! Collecting items will help you to get
        through all the hardships and make MeowRun much more exciting." />
    <Feature 
        iconSource="/icons/map.gif"
        title="Massive 3D map"
        paragraph="Your character have the highest level of freedom to explore MeowRun map. Be ready for cool adventure!" />
    <Feature 
        iconSource="/icons/vote.gif"
        title="Vote your character"
        paragraph="You have all the right.. especially to vote for MeowRun next player! Please help us to decide which character
        should be the main player for uncoming update!" />
    </div>
    <PlayButton 
        buttonText="Play MeowRun"
        jumpTo="playMeowRun" />
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #whyMeowRun {
            text-align: center;
            margin-bottom: 30px;
        }
    @media screen and (min-width:$tablet) {
        
        h1 {
            text-align: center;
            font-size: $unit *3;
        }
        #features {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            grid-template-rows: repeat(2 1fr);
            gap: $unit*2;
            padding: $unit;
            justify-content: center;
            align-items: center;
        }
    }

</style>