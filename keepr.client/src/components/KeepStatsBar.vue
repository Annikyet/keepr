<template>
  <div class="component">
    <div class="d-flex justify-content-center">
      <span class="mx-2"><i class="mx-1 text-primary mdi mdi-eye"></i><span class="stats-text">{{keep.views}}</span></span>
      <span class="mx-2"><i class="mx-1 text-primary mdi mdi-lock"></i><span class="stats-text">{{keep.kept}}</span></span>
      <span class="mx-2"><i class="mx-1 text-primary mdi mdi-connection"></i><span class="stats-text">0</span></span>
    </div>

  </div>
</template>


<script>
export default {
  props: {
    keep: {
      id: Number,
      creatorId: String,
      name: String,
      description: String,
      img: String,
      views: Number,
      kept: Number,
      creator: {
        id: String,
        name: String,
        picture: String
      }
    }
  },
  setup(props){
    return {

    }
  }
}
</script>


<style lang="scss" scoped>

.stats-text {
  color: #606060;
  font-weight: 400;
}


</style>