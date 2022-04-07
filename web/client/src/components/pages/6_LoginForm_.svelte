<script lang="ts">

    const handleSubmit = () => {
        // preventDefault => applied by event modifier
        const loginForm = document.forms[0]
        console.log(loginForm.elements.namedItem("email"))
}
</script>


<main id="loginPage">

    <div class="welcomeImage">
        <img src="" alt="asdf" />
    </div>

    <form id="loginForm" name="loginForm" on:submit|preventDefault={handleSubmit}>
        <span id="greetings">Welcome</span>
        <p>Log in to MeowRun to continue</p>
        <fieldset>
            <legend>Email address</legend>
            <input type="email" name="email" id="email" placeholder="abcd@gmail.com">
            <p class="validationMsg">email validation here</p>
        </fieldset>
        <fieldset>
            <legend>Password</legend>
            <input type="password" name="password" id="password" placeholder="more than 6 characters">
            <p class="validationMsg">password validation here</p>
        </fieldset>
        <button>Sign in</button>

        <!-- TO DO: fix url later -->
        <u><i><a href="www.google.com">Forgot password?</a></i></u>
    </form>

</main>


<style lang="scss">
    @import '../partials/common';

    #loginPage {
        @include gridTwoColumns();
        align-items: center;
    }


    #greetings {
        font-size: $unit*1.5;
        font-weight: bold;
    }
    #loginForm { 
        @include flexColumn();
        background-color: white;
        justify-content: center;
        align-items: center;
        gap: $unit;
        padding: $unit;
        fieldset { 
            width: fit-content;
        }
        button { 
            background-color: rgb(37, 146, 255);
            color: white;
            padding: $unit*0.8 $unit;
            left: 0;
        }
    }
    .validationMsg {
        margin: 0; 
        padding: 0;
        color: tomato;
    }
</style>