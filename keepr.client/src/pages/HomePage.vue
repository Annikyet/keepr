<template>
  <!-- TODO add masonry later -->
  <div class="container-fluid px-md-5 pt-4">
    <div class="row">
      <div v-for="k in keeps" :key="k.id" @click="viewKeep(k)" class="col-6 col-md-4 col-lg-3 p-2">
      <!-- <img src="../assets/img/404.svg" alt=""> TODO use this as default img in DB-->
        <KeepTile :keep="k" />
      </div>
    </div>
  </div>
  <KeepModal :keep="activeKeep" />
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import { Modal } from 'bootstrap'
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.GetAll()
        // document.getElementById('keepModal').
        
      } catch (error) {
        logger.error(error)
      }
    })

    return {
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),

      viewKeep(keep) {
        AppState.activeKeep = keep
        Modal.getOrCreateInstance(document.getElementById('keepModal')).show()
        
        
      }
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>