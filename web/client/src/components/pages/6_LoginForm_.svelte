<script lang="ts">
    import axios from 'axios'
    import API from '../API/url'
    import {username} from '../store/store'
    
    let validationMsg = {
        email: 'currently empty', 
        password: 'currently empty'
    }

    const handleSubmit = async () => {
        // preventDefault => applied by event modifier
        const loginForm = document.forms[1]
        const email = loginForm.elements.namedItem("email") as HTMLInputElement
        const password = loginForm.elements.namedItem("password") as HTMLInputElement
        
        const res = await axios.post(API.AUTH.login, { 
            email: email.value,
            password: password.value 
        })

        console.log(res)
        
        if (res.status === 200) {
            // check server response and update svelte store for user email
            username.update((prev) => prev = email.value)
        }

        // TO DO : add validateAuth() later
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

    <div id="welcomeImage">
        <img src="https://mir-s3-cdn-cf.behance.net/project_modules/1400/d4b335139168569.62301313cb03b.jpg" alt="welcome banner" loading='lazy'/>
    </div>

    <form id="loginForm" name="loginForm" on:submit|preventDefault={handleSubmit}>
        <span id="greetings">Login</span>
        <p>Log in to MeowRun to continue</p>
        <fieldset>
            <legend>Email address</legend>
            <input type="email" name="email" id="email" placeholder="abcd@gmail.com">
            <p class="validationMsg">{validateAuth("email")}</p>
        </fieldset>
        <fieldset>
            <legend>Password</legend>
            <input type="password" name="password" id="password" placeholder="more than 6 characters">
            <p class="validationMsg">{validateAuth("password")}</p>
        </fieldset>

        <div id="buttons">
            <button id="signin">Sign in</button>
            <button id="signup">Sign up</button>
        </div>

        <!-- TO DO: fix url later -->
        <u><i><a href="www.google.com">Forgot password?</a></i></u>
    </form>

</main>


<style lang="scss">
    @import '../partials/common';

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
            max-width: 95%;
            height: auto;
        }
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
        button { 
            background-color: rgb(37, 146, 255);
            color: white;
            padding: $unit*0.8 $unit*1.5;
            left: 0;
            border-radius: 50px;
        }
        #signup { 
            background-color: rgb(6, 161, 48);
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
</style>