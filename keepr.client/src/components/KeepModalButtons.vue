<template>
  <div class="d-flex justify-content-between align-items-end">

    <div class="d-flex align-items-end">

      <div v-show="account.id" class="btn-group me-5" role="group">
        <button id="addToVaultGroup" class="btn btn-outline-primary dropdown-toggle" data-bs-toggle="dropdown"
          aria-expanded="false">Add to Vault</button>
        <ul class="dropdown-menu" aria-labelledby="addToVaultGroup">
          <li v-for="v in myVaults" :key="v.id"><a @click="addToVault(v.id)" class="dropdown-item">{{v.name}}</a></li>
        </ul> 
      </div>

      <!-- account is empty... -->

      <i v-show="account.id == keep.creatorId" @click="deleteKeep()" class="mdi mdi-delete-outline text-secondary" title="Delete"></i>

    </div>

    <div @click="viewProfile(keep?.creator)" class="d-flex align-items-end">
      <img :src="keep?.creator?.picture" :alt="keep?.creator?.name" class="profile-pic rounded-3 me-2">
      <p class="mb-0 profile-text">{{ keep?.creator?.name }}</p>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState'
import { computed, onMounted } from 'vue'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger.js'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
import { vaultKeepsService } from '../services/VaultKeepsService'
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
    const router = useRouter()
    return {
      account: computed(() => AppState.account ),
      myVaults: computed(() => AppState.myVaults),

      viewProfile() {
        // console.log(user.name)
        // AppState.activeProfile = props.keep.creator
        Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
        // logger.log(props.keep.creator.id)
        router.push({ name: "Profile", params: { id: props.keep.creator.id } });
      },

      async deleteKeep() {
        try {
          await keepsService.delete(props.keep.id)
          Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
          
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },

      async addToVault(vaultId) {
        try {
          await vaultKeepsService.create(props.keep.id, vaultId)
          Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
          
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-pic {
  height: 36px;
  width: 36px;
  object-fit: contain;
}

.profile-text {
  font-size: 18px;
  font-weight: 300;
}

.mdi-delete-outline {
  font-size: 36px;
  line-height: 36px;
}
</style>