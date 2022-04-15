<script lang="ts" >
import PlayButton from "./PlayButton.svelte";
</script>


<nav id="navigation">
    <a href="/">
        <div id="logo">
            <img src="/meowRunFavicon.png" alt="logo">
            <span>MeowRun</span>
        </div>
    </a> 
    <ul id="navigationBar">
        <li><a href="#main">Main</a></li>
        <li><a href="#about">About</a></li>
        <li><a href="#whyMeowRun">Why MeowRun</a></li>
        <li><a href="#howItWorks">How It Works</a></li>
        <li><a href="#characterVote">Character Vote</a></li>
    </ul>
    <div id="navButton">
        <PlayButton 
            isTransparent={false}
            jumpTo="loginPage"
            buttonText="Login"/>
    </div>
</nav>


<style lang="scss">
    @import '../partials/common.scss';
    #navigation {
        text-align: center;
    }
    #logo {
        @include flexRow();
        justify-content: space-between;
        align-items: center;
        padding: 0 $unit*1.5;
        img { width: 100px; }
        background-color: $layoutColor;
    }
    #navigationBar, #navButton {
        display: none;
    }
    .active { 
        @include flexColumn();
        justify-content: left;
        align-items: center;
        font-size: $unit *1.3;
        background-color: $subBgColor;
    }
    @media screen and (min-width: $tablet) {
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
                    min-width: max(100px, 18%);
                    height: auto;
                }            span { 
                    font-size: $unit*1.5;
                }
            }
            #navButton { display: block; }
        }
        #navigationBar {
            @include flexRow();
            gap: $unit*1.2;
            font-size: large;
            background-color:transparent;
        }

    }
</style>