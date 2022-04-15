<script lang="ts">

</script>


<footer id="footer">
<ul id="project">
    <li class="title">Project</li>
    <ul>
        <li class="footerItem"><a href="https://github.com/DaeguDev/MeowRun">Readme</a></li>
        <li class="footerItem"><a href="https://github.com/DaeguDev/MeowRun/issues">Troubleshooting</a></li>
        <li class="footerItem"><a href="https://github.com/DaeguDev/MeowRun/discussions">Discussion</a></li>
    </ul>

</ul> 
<ul id="contributor">
    <li class="title">Contributor</li>
    <ul>
        <li class="footerItem"><a href="https://github.com/jshhhhh">Seokhwan Jung</a></li>
        <li class="footerItem"><a href="https://github.com/omago123">Jungi Kim</a></li>
        <li class="footerItem"><a href="https://github.com/developerasun">Jake Sung</a></li>
    </ul>
</ul>
</footer>


<style lang="scss">
    @import '../partials/common.scss';
    #footer {
        @include flexColumn();
        background-color: $subBgColor;
        text-align: center;
        padding: $unit*1.3;
        .title {
            font-size: $unit * 1.6;
            font-weight: bold;
            padding: $unit;
        }
        .footerItem {
            font-size: $unit * 1.2;
            padding: $unit;
            border: 2px solid;
            border-image : linear-gradient(to right, #1B51F2 0%, #F263DA 100%);
            border-image-slice : 4;
        }
    }
    @media screen and (min-width:$tablet) {
        #footer { 
            display: grid;
            grid-template-columns: repeat(2, 1fr);
        }
        .footerItem {
            width:auto;
        }
    }

    
</style>