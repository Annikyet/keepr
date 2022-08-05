<template>
  <div class="component">
    <div class="container">
      <div class="row mt-5">
        <div class="col-12 d-flex justify-content-between">
          <div>
            <h4 class="vault-title">{{ vault.name }}</h4>
            <h6 class="vault-stats">Keeps: {{ keeps.length }}</h6>
          </div>
          <div class="d-flex align-items-start">
            <button v-if="account.id == vault.creatorId" @click="deleteVault()" class="btn btn-outline-secondary">Delete Vault</button>
          </div>
        </div>
      </div>
      <div class="masonry-frame mt-5">
        <div v-for="k in keeps" :key="k.id" @click="viewKeep(k)" class="p-2 ps-0">
          <KeepTile :keep="k" />
        </div>
      </div>
    </div>
  <KeepModal :keep="activeKeep">
    <VaultKeepModalButtons :keep="activeKeep" />
  </KeepModal>
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
import KeepModalButtons from '../components/KeepModalButtons.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
export default {
  setup() {
    const route = useRoute();
    const router = useRouter()
    onMounted(async () => {
      try {
        await vaultsService.getVault(route.params.id);
        await vaultsService.getVaultKeeps(route.params.id);
      } catch (error) {
        logger.error(error)
        Pop.toast(error) // .error has issues
        router.push({name: 'Home'})
      }
    });
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),

      async viewKeep(keep) {
        try {
          // AppState.activeKeep = keep
          // make API call to trigger view counter
          // debugger
          await keepsService.getById(keep.id)
          AppState.activeKeep = keep
          Modal.getOrCreateInstance(document.getElementById('keepModal')).show()
          
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },

      async deleteVault() {
        try {
          await vaultsService.delete(AppState.activeVault.id, AppState.vaultKeeps)
          router.push({ name: "Profile", params: { id: AppState.account.id } })
          
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      }
    };
  },
  components: { KeepTile, KeepModal, KeepModalButtons }
}
</script>


<style lang="scss" scoped>
.masonry-frame {
  columns: 4;

  div {
    break-inside: avoid;
  }
}

@media (max-width: 991.98px) {
  .masonry-frame {
    columns: 3;
  }
}

@media (max-width: 767.98px) {
  .masonry-frame {
    columns: 2;
  }
}

.vault-title {
  font-size: 48px;
  font-weight: 300;
}

.vault-stats {
  font-size: 20px;
  font-weight: 300;
}
</style>