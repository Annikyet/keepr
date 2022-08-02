<template>
  <div @click="" class="component">
      <div class="card bg-dark text-white">
        <!-- TODO check if keep.img is a valid image -->
        <img :src="keep.img" class="card-img" :alt="keep.name">
        <div class="card-img-overlay d-flex justify-content-between align-items-end">
          <h5 class="card-title">{{ keep.name }}</h5>
          <a href="#">
            <img :src="keep.creator.picture" :alt="keep.creator.name" class="rounded-pill img-fluid">
          </a>
        </div>
      </div>
  </div>
</template>


<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'
import { Keep } from "../models/Keep.js"
export default {
  props: {
    keep: {
      type: Keep
    }
  },
  setup(props) {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.card-img {
  min-height: 160px;
}

.card-img-overlay img {
  max-height: 36px;
}
</style>