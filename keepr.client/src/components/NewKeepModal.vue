 <template>
  <div class="component">
    <div class="modal fade" id="newKeepModal" tabindex="-1" aria-labelledby="newKeepModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="cardp p-3">
            <div class="d-flex justify-content-end">
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <h3 class="card-title">New Keep</h3>
            <form @submit.prevent="createKeep()">
            <div class="mb-1">
              <label for="keepTitle" class="form-label">Title</label>
              <input type="text" id="keepTitle" name="keepTitle" v-model="name" placeholder="Title..."
                class="form-control">
            </div>
            <div class="mb-1">
              <label for="keepImg" class="form-label">Image URL</label>
              <input type="text" id="keepImg" name="keepImg" v-model="img" placeholder="URL..." class="form-control">
            </div>
            <div class="mb-1">
              <label for="vaultIsPrivate" class="form-check-label">Description</label>
              <textarea name="keepDescription" id="keepDescription" v-model="description"
                placeholder="Keep Description..." rows="5" class="form-control"></textarea>
            </div>
            <!-- <div class="mb-1">
              <label for="keepTags" class="form-label">Tags</label>
              <input type="text" id="keepTags" name="keepTags" v-model="img" placeholder="Tags..." class="form-control">
            </div> -->
            <button type="submit" class="btn btn-primary">Create</button>
            </form>
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
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    return {
      name: "",
      img: "",
      description: "",
      tags: "",

      async createKeep() {
        try {
          let newKeep = {
            name: this.name,
            img: this.img,
            description: this.description
            // ,tags: this.tags
          }
          await keepsService.create(newKeep)
          Modal.getOrCreateInstance(document.getElementById("newKeepModal")).hide()
          
        } catch (error) {
          logger.error(error)
          Pop.toast(error0)
        }
      }
    }
  }
}
</script>
 
 
 <style lang="scss" scoped>
 </style>