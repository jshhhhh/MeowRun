<script lang="ts">
	import LayoutProvider from './components/sub/LayoutProvider.svelte'
	import Main from "./components/pages/1_Main_.svelte";
	import About from "./components/pages/2_About_.svelte";
	import WhyMeowRun from './components/pages/3_WhyMeowRun_.svelte';
	import HowItWorks from './components/pages/4_How_it_works_.svelte';
	import CharacterVote from './components/pages/5_CharacterVote_.svelte';
	import LoginForm from './components/pages/6_LoginForm_.svelte';
</script>

<main>
	<LayoutProvider>
		<Main slot="1_Main_" />
		<About slot="2_About_" />
		<WhyMeowRun slot="3_WhyMeowRun_" />
		<HowItWorks slot="4_How_it_works_" />
		<CharacterVote slot="5_CharacterVote_" />
		<LoginForm slot="6_LoginForm_" />
	</LayoutProvider>
</main>

<style lang="scss">

	main {
		box-sizing: border-box;
		padding: 0;
		margin: 0;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>