
<template>

    <HeaderPart/>

    <MenuPart />

    <FooterPart/>

</template>

<script>

import HeaderPart from '@/components/Header.vue'

import FooterPart from '@/components/Footer.vue'

import MenuPart from '@/components/Menu.vue'

export default{
    name : "IndexPage",
    components:{
        HeaderPart,
        FooterPart,
        MenuPart
    }
}

</script>

<style scoped>

</style>