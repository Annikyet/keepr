<template>
  <div class="component">
    <div class="container">
      <div class="row">
        <div class="d-flex">
          <img :src="profile?.picture" :alt="profile?.name" class="profile-pic">
          <div class="d-flex flex-column">
            <h3>{{profile?.name}}</h3>
            <h5>Vaults: {{vaults?.length}}</h5>
            <h5>Keeps: {{keeps?.length}}</h5>
          </div>
        </div>
      </div>
      
      <div class="row">
        <h4>
          Vaults
          <i class="mdi mdi-plus"></i>
        </h4>
        <div class="col-12">
          <div class="row">
            <div v-for="v in vaults" :key="v.id" @click="viewVault(v)" class="col-6 col-md-3 col-lg-2 p-2">
              <VaultTile :vault="v" />
            </div>
          </div>
        </div>
      </div>

      <div class="row">
        <h4>
          Keeps
          <i class="mdi mdi-plus"></i>
        </h4>
      </div>
  <!-- masonry goes here -->
      <div class="masonry-frame">
        <ProfileKeepTile v-for="k in keeps" :key="k.id" :keep="k" @click="viewKeep(k)" class="py-2" />
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
import VaultTile from '../components/VaultTile.vue';
export default {
    setup() {
        const router = useRouter();
        const route = useRoute();
        onMounted(async () => {
            try {
                await profilesService.getProfile(route.params.id);
                await profilesService.getVaults(AppState.activeProfile.id);
                await profilesService.getKeeps(AppState.activeProfile.id);
            }
            catch (error) {
                logger.error(error);
            }
        });
        return {
            // no curly gorls
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.profileKeeps),
            vaults: computed(() => AppState.profileVaults),
            activeKeep: computed(() => AppState.activeKeep),

            async viewKeep(keep) {
                keep.creator = AppState.activeProfile;
                AppState.activeKeep = keep;
                Modal.getOrCreateInstance(document.getElementById("keepModal")).show();
            },

            async viewVault(vault) {
              router.push({name: 'Vault', params: {id: vault.id}})
            }
        };
    },
    components: { VaultTile }
}
</script>


<style lang="scss" scoped>
.profile-pic {
  height: 200px;
  width: 200px;
  object-fit: contain;
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