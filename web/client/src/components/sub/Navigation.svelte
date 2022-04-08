<script lang="ts">
const handleClick = () => {
    // TO DO 
}
</script>


<nav id="navigation">
    <div id="logo">
        <img src="/meowRunFavicon.png" alt="logo">
        <span>MeowRun</span>
    </div>
    <ul id="navigationBar">
        <li><a href="#main">Main</a></li>
        <li><a href="#about">About</a></li>
        <li><a href="#whyMeowRun">Why MeowRun</a></li>
        <li><a href="#howItWorks">How It Works</a></li>
        <li><a href="#characterVote">Character Vote</a></li>
    </ul>
    <div id="auth">
        <a href="#loginPage" on:click={handleClick}><button>Login</button></a>
    </div>
</nav>


<style lang="scss">
    @import '../partials/common.scss';
    #navigation {
        @include flexRow();
        flex-wrap: nowrap;
        justify-content: space-around;
        align-items: center;
        background-color: $layoutColor;
        color: white;
        font-size: $unit;
        margin: 0;
        padding: 0;
        #logo { 
            @include flexRow();
            align-items: center;
            img { 
                max-width: 18%;
                height: auto;
            }            span { 
                font-size: $unit*1.5;
            }
        }
    }
    #navigationBar {
        @include flexRow();
        gap: $unit*1.2;
        font-size: large;
    }
</style>