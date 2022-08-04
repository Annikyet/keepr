<template>
  <!-- TODO add masonry later -->
  <div class="container-fluid px-md-5 pt-4">
    <div class="masonry-frame">
      <div v-for="k in keeps" :key="k.id" @click="viewKeep(k)" class="py-2">
      <!-- <img src="../assets/img/404.svg" alt=""> TODO use this as default img in DB-->
        <KeepTile :keep="k" />
      </div>
    </div>
  </div>
  <KeepModal :keep="activeKeep">
    <KeepModalButtons :keep="activeKeep" />
  </KeepModal>
</template>

<script>
import { computed, onMounted, ref } from 'vue'
import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import { Modal } from 'bootstrap'
import KeepModalButtons from '../components/KeepModalButtons.vue'
export default {
    name: "Home",
    setup() {
        onMounted(async () => {
            try {
                await keepsService.getAll();
                // document.getElementById('keepModal').
            }
            catch (error) {
                logger.error(error);
            }
        });
        return {
            keeps: computed(() => AppState.keeps),
            activeKeep: computed(() => AppState.activeKeep),
            viewKeep(keep) {
                AppState.activeKeep = keep;
                Modal.getOrCreateInstance(document.getElementById("keepModal")).show();
            }
        };
    },
    components: { KeepModalButtons }
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

.masonry-frame {
  columns: 6;

  div {
    break-inside: avoid;
  }
}

@media (max-width: 991.98px) {
  .masonry-frame {
    columns: 4
  }
}

@media (max-width: 767.98px) {
  .masonry-frame {
    columns: 2;
  }
}
</style>