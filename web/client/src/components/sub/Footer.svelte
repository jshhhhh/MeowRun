<script lang="ts">

</script>


<footer id="footer">
<ul id="project">
    <li class="title">Project</li>
    <ul>
        <li><a href="https://github.com/DaeguDev/MeowRun">Readme</a></li>
        <li><a href="https://github.com/DaeguDev/MeowRun/issues">Troubleshooting</a></li>
        <li><a href="https://github.com/DaeguDev/MeowRun/discussions">Discussion</a></li>
    </ul>

</ul> 
<ul id="contributor">
    <li class="title">Contributor</li>
    <ul>
        <li><a href="https://github.com/jshhhhh">Seokhwan Jung</a></li>
        <li><a href="https://github.com/omago123">Jungi Kim</a></li>
        <li><a href="https://github.com/developerasun">Jake Sung</a></li>
    </ul>
</ul>
</footer>


<style lang="scss">
    @import '../partials/common.scss';

    #footer { 
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        background-color: $subBgColor;
        text-align: center;
        padding: $unit*1.3;
        .title {
            font-size: $unit * 1.3;
            font-weight: bold;
            padding-bottom: $unit;
        }
    }
</style>