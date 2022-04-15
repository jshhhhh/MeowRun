<script lang="ts">
    import axios from 'axios'
    import API from '../API/url'
    import { user } from '../store/store'
    import PlayButton from '../sub/PlayButton.svelte'
    import TabProvider from '../sub/TabProvider.svelte'
    
    let validationMsg = {
        email: 'currently empty', 
        password: 'currently empty'
    }
    
    // bind JS value with form value
    let bindEmail=""
    let bindPassword=""

    const handleSubmit = async () => {
        // preventDefault => applied by event modifier
        const loginForm = document.forms[1]
        console.log(loginForm)
        const email = loginForm.elements.namedItem("email") as HTMLInputElement
        const password = loginForm.elements.namedItem("password") as HTMLInputElement

        bindEmail = email?.value
        bindPassword = password?.value

        // send login info to server
        const res = await axios.post(API.AUTH.login, { 
            email: email?.value,
            password: password?.value 
        })

        console.log(res)
        
        if (res.status === 200) {
            // check server response and update svelte store for user email
            user.update((prev) => prev = { username: email.value, isLogin: true })

            // set session item to keep svelte state even after page refresh
            sessionStorage.setItem("MeowRunUser", JSON.stringify({ username: email.value, isLogin: true }))
        } else { 
            const { errorType } = res.data
            const message = validateAuth(errorType)

            if (message.includes("email")) validationMsg.email = message
            else validationMsg.password = message
        }

        // initialize form again
        bindEmail = ""
        bindPassword = ""
        
    }

    // check server validation error message and return the message to display
    const validateAuth = (errorType:any) => {
        switch(errorType) {
            case "email": 
                // update email validation message
                return validationMsg.email
            case "password":
                // update password validation message
                return validationMsg.password
            default:
                return ""        
        }
    }
</script>


<main id="loginPage">
    <!-- TO DO: add tabs for login / sign up and conditional rendering -->
    <div id="welcomeImage">
        <img src="https://mir-s3-cdn-cf.behance.net/project_modules/1400/d4b335139168569.62301313cb03b.jpg" alt="welcome banner" loading='lazy'/>
    </div>

    <form id="loginForm" name="loginForm" on:submit|preventDefault={handleSubmit}>
        <span id="greetings">Join MeowRun family</span>
        <p>
            Login here using your email and password. Login user has voting capability for upcoming game character selection. 
            Don't hesitate to become one of the coolest MeowRun members!
        </p>
        <fieldset>
            <legend>Email address</legend>
            <input type="email" name="email" id="email" placeholder="abcd@gmail.com" bind:value={bindEmail}>
            <p class="validationMsg">{validateAuth("email")}</p>
        </fieldset>
        <fieldset>
            <legend>Password</legend>
            <input type="password" name="password" id="password" placeholder="more than 8 characters" bind:value={bindPassword}>
            <p class="validationMsg">{validateAuth("password")}</p>
        </fieldset>

        <div id="buttons">
            <PlayButton 
            buttonText="Login"
            jumpTo="" 
            isTransparent={false} />
            <PlayButton 
            buttonText="Sign up"
            jumpTo="" />
        </div>

        <!-- TO DO: fix url later -->
        <u><i><a href="www.google.com">Forgot password?</a></i></u>
    </form>
</main>


<style lang="scss">
    @import '../partials/common';
    #loginPage { 
        @include flexColumn();
        justify-content: center;
        align-items: center;
        img { display: none; }
        #greetings { text-align: center; }
    }
    #loginForm { 
            @include flexColumn();
            justify-content: center;
            align-items: center;
            gap: $unit;
            padding: $unit;
            fieldset {
                width: 75%;
            }
            input { 
                width: 100%;
                margin-top: $unit*0.7;
            }
            #buttons { 
                @include flexRow();
                gap: $unit;
            }
            #greetings {
                font-size: $unit*3;
                font-weight: bold;
            }
        }
        .validationMsg {
            margin: 0; 
            padding: 0;
            color: tomato;
        }
    @media screen and (min-width:$tablet) {
        #loginPage {
            @include gridTwoColumns();
            align-items: center;
            justify-content: center;
            text-align: center;
            background-color: $subBgColor;
            color: white;
            padding:$unit*2;
        }
        
        #welcomeImage { 
            img { 
                display: block;
                max-width: 95%;
                height: auto;
            }
        }
    }
</style>