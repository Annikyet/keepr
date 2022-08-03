<template>
  <div class="component">
    <div class="card text-white">
      <img :src="vault.img" :alt="vault.name" class="vault-img">
      <div class="card-img-overlay d-flex align-items-end">
        <h5 class="card-title">{{vault.name}}</h5>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    vault: {
      id: Number,
      creatorId: String,
      name: String,
      description: String,
      img: String,
      isPrivate: Boolean,
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

.vault-img {
  height: 160px;
  object-fit: cover;
}

</style>