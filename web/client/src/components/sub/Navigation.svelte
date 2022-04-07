<script lang="ts">
const handleClick = () => {
    // TO DO 
}
</script>


<main id="navigation">
    <div id="logo">
        <img src="" alt="logo">
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
</main>


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
    }
    #navigationBar {
        @include flexRow();
        gap: $unit;
    }
</style>