<template>
  <div class="component">
    Profile
    {{ profile?.name }}

  </div>
</template>


<script>
export default {
  props: {
    profile: {
      id: String,
      name: String,
      picture: String
    }
  },
  setup(props){
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>