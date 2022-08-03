<template>
  <div @click="viewKeep()" class="component">
    <div class="card bg-dark text-white">
      <!-- TODO check if keep.img is a valid image -->
      <img :src="keep.img" class="card-img" :alt="keep.name">
      <div class="card-img-overlay d-flex justify-content-between align-items-end">
        <h5 class="card-title">{{ keep.name }}</h5>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, ref } from 'vue'
import { router } from '../router.js'
import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'

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
  setup(props) {
    return {
      viewKeep() {
        AppState.activeKeep = props.keep;
        // console.log(props.keep.id)
        // router.push({name: 'Keep'})
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.card-img {
  min-height: 160px;
  max-height: 80vh;
  object-fit: cover;
}

.profile-pic {
  height: 36px;
}
</style>