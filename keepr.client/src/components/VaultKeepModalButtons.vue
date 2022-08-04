<template>
  <div class="d-flex justify-content-between align-items-end">
    <button v-if="account.id == vault.creatorId" @click="removeFromVault(keep.id)" class="btn btn-outline-secondary">Remove from Vault</button>

    <i v-show="account.id == keep.creatorId" @click="deleteKeep()" class="mdi mdi-delete-outline text-secondary" title="Delete"></i>

    <div @click="viewProfile(keep?.creator)" class="d-flex align-items-end">
      <img :src="keep?.creator?.picture" :alt="keep?.creator?.name" class="profile-pic rounded-3 me-2">
      <p class="mb-0 profile-text">{{ keep?.creator?.name }}</p>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState'
import { computed } from 'vue'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger.js'
import { useRouter } from 'vue-router'
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
      vaultKeepId: Number,
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
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),

      viewProfile() {
        // console.log(user.name)
        // AppState.activeProfile = props.keep.creator
        Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
        // logger.log(props.keep.creator.id)
        router.push({ name: "Profile", params: { id: props.keep.creator.id } });
      },

      async removeFromVault() {
        await vaultKeepsService.delete(props.keep.vaultKeepId)
        Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
      },

      async deleteKeep() {
        await keepsService.delete(props.keep.id)
        Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
        // do more things
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