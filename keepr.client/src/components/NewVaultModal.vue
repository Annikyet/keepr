 <template>
  <div class="component">
    <div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="newVaultModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="cardp p-3">
            <div class="d-flex justify-content-end">
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <h3 class="card-title">New Vault</h3>
            <!-- <form> -->
              <div class="mb-1">
                <label for="vaultTitle" class="form-label">Title</label>
                <input type="text" id="vaultText" name="vaultText" v-model="name" placeholder="Title..." class="form-control">
              </div>
              <div class="mb-1">
                <label for="vaultImg" class="form-label">Image URL</label>
                <input type="text" id="vaultImg" name="vaultImg" v-model="img" placeholder="URL..." class="form-control">
              </div>
              <div class="mb-1">
                <input type="checkbox" id="vaultIsPrivate" name="vaultIsPrivate" v-model="isPrivate" class="form-check-input">
                <label for="vaultIsPrivate" class="form-check-label">Private?</label>
                <p id="vaultIsPrivateInline" class="form-text">Private Vaults can only be seen by you</p>
              </div>
              <button @click="createVault()" class="btn btn-primary">Create</button>
            <!-- </form> -->
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
 
 
 <script>
import { AppState } from '../AppState'
import { computed } from 'vue'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger.js'
import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
export default {
  setup() {
    return {
        name: "",
        img: "",
        isPrivate: false,

      async createVault() {
        try {
          let newVault = {
              name: this.name,
              img: this.img,
              isPrivate: this.isPrivate
            }
  
          await vaultsService.createVault(newVault)
          Modal.getOrCreateInstance(document.getElementById("newVaultModal")).hide();
          
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
 
 </style>