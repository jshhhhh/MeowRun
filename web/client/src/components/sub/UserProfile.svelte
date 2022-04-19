<script lang="ts">
import axios from 'axios'
import API_URL from '../API/url';
import PlayButton from "./PlayButton.svelte";
import { user } from '../store/store'
import Avatar from 'svelte-avatar'

const handleLogout = async () => {
    const res = await axios.delete(API_URL.AUTH.logout)
    if (res.status === 200 || 204) {
        // reset svelte store
        user.set({username : 'NoId@gmail.com', isLogin: false})
        // reset session item
        sessionStorage.removeItem("MeowRunUser")
        alert("Logout success")
        window.location.reload()
    }
}

</script>

<main id="userProfile">
    <div id="notification">
        <h1>Oops.. gotta login first.</h1>
        <img
        id="loginDirective"
        loading='lazy'
        src="https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/9381d046278873.5c4704ab2cddf.png" 
        alt="login directive">
    </div>
    <!-- check svelte store and session item -->
    {#if $user.isLogin && JSON.parse(sessionStorage.getItem("MeowRunUser")).isLogin}
    <div id="menus">
        <Avatar 
            src="https://no.image.exist"
            size={"8rem"}
            name={$user.username}
            randomBgColor
            textColor="white" />

        <p>Welcome back, {$user.username}</p>

        <div id="buttons">
            <PlayButton
                buttonText='Vote'
                jumpTo='characterVote' />
            <PlayButton
                callback={handleLogout}
                isTransparent={false}
                buttonText='Logout'
                jumpTo='' />
        </div>
    </div>
    {/if}
</main>

<style lang="scss">
    @import '../partials/common.scss';
    #notification { 
        text-align: center; 
        border: 2px solid white; 
        width: fit-content; 
        margin: 0 auto;
    }
    #loginDirective { border-radius: 50%; max-width: 50%; height: auto; }
    #menus { 
        @include flexColumn();
        justify-content: center;
        align-items: center;
    }
    #buttons { @include flexRow(); gap: $unit; }

    @media screen and (min-width: $tablet) {
        
    }
</style>