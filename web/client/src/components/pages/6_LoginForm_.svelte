<script lang="ts">
    import TabProvider from '../sub/TabProvider.svelte'
    
    // deliver props to TabProvider
    let tabList = ["Login", "Profile"]
    let current = "Login" // default tab is Login

</script>

<main id="loginPage">
    <TabProvider 
        {tabList}
        {current} />
</main> 


<style lang="scss">
</style>