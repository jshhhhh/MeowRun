<script lang="ts">
    const TABLET = 640;
    let currentWindowSize:number = window.innerWidth;
    let isMobile:boolean = currentWindowSize < TABLET ? true : false;

    // calculate window size for conditional rendering
    const calculateScreenSize = () => {
        window.addEventListener("resize", () => {
            currentWindowSize = window.innerWidth  
            if (currentWindowSize < TABLET) isMobile = true 
            else isMobile = false
        })
        return isMobile
    }

    isMobile = calculateScreenSize()
</script>
    
<main id="playMeowRun">
{#if isMobile}
    <p id="message">Can't play game in mobile.</p>
    {:else}
    <h1>Play MeowRun Anywhere</h1>
    <!-- FIX: change video size later -->
    <iframe 
        id="simmerioTag"
        title="jumping car"
        src="https://i.simmer.io/@developerasun/jumpingcar2" >
    </iframe>
    <p>
        We have put a tons of efforts to develop MeowRun and proudly would like to say this is a 
        enjoyable game for everyone. Move your character with arrow key and collect awesome items
        to improve your power! 
        <i>Note that the game is currently only playable in web</i>
    </p>
{/if}
</main>


<style lang="scss">
    @import '../partials/common.scss';
    #playMeowRun {
        text-align: center;
        padding: $unit *4;
        h1 { font-size: $unit*3; margin-top: 0; padding-top: 0;}
        p { text-align: left;}
        #message { text-align: center;}
    }

    #simmerioTag { display: none;}

    @media screen and (min-width: $tablet) {
        #simmerioTag { 
            display: block;
            width: 600px;
            height: 400px;
            margin: 0 auto;
        }
    }
    @media screen and (min-width: $pc) {
        #simmerioTag {
            width: 800px;
            height: 600px;
        }
    }
</style>