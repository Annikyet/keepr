<template>
  <div class="component">
    <div class="container">
      <div class="row">
        <div class="col-12 d-flex justify-content-between">
          <div>
            <h4>{{ vault.name }}</h4>
            <h6>Keeps: vaultKeep stoufe</h6>
          </div>
          <i @click="" class="mdi mdi-delete"></i>
        </div>
      </div>
      <div class="masonry-frame">
        <div v-for="k in keeps" :key="k.id" @click="viewKeep(k)" class="p-2">
          <KeepTile :keep="k" />
        </div>
      </div>
    </div>
  <KeepModal :keep="activeKeep" />
  </div>
</template>


<script>
import { profilesService } from '../services/ProfilesService'
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState';
import { logger } from '../utils/Logger.js'
import { useRouter, useRoute } from 'vue-router'
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService'
import KeepTile from '../components/KeepTile.vue';
import KeepModal from '../components/KeepModal.vue';
export default {
  setup() {
    const route = useRoute();
    onMounted(async () => {
      await vaultsService.getVault(route.params.id);
      await vaultsService.getVaultKeeps(route.params.id);
    });
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
      activeKeep: computed(() => AppState.activeKeep),

      viewKeep(keep) {
        AppState.activeKeep = keep
        Modal.getOrCreateInstance(document.getElementById('keepModal')).show()
      }
    };
  },
  components: { KeepTile, KeepModal }
}
</script>


<style lang="scss" scoped>
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