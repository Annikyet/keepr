 <template>
  <div class="component">
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content border-0">
          <div class="card border-0">
            <div class="row">
              <div class="col-6">
                <img :src="keep?.img" :alt="keep?.name" class="img-fluid">
              </div>
              <div class="col-6">
                <div class="card-body">
                  <div class="d-flex flex-column justify-content-between">
                    <div>
                      <div class="d-flex justify-content-end">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>
                      <div class="d-flex justify-content-center">
                        <p>social medi</p>
                        <p>social medi</p>
                        <p>social medi</p>
                      </div>
                      <h5 class="card-title text-center">{{ keep?.name }}</h5>
                      <p class="card-text">{{ keep?.description }}</p>
                    </div>
                    <hr>
                    <div class="d-flex justify-content-between">
                      <button class="btn btn-primary">add to vault</button>
                      <!-- account is empty... -->
                      <button v-show="account?.id == keep?.creator?.id" class="btn btn-primary">trashcan</button>
                      <div @click="viewProfile(keep?.creator)">
                        <img :src="keep?.creator?.picture" :alt="keep?.creator?.name">
                        <p>{{ keep?.creator?.name }}</p>
                      </div>  
                    </div>
                  </div>
                  <!-- tags -->
                </div>
              </div>
            </div>
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
    // console.log(AppState.account)
    const router = useRouter()
    return {
      account: computed(() => {AppState.account}),
      viewProfile() {
        // console.log(user.name)
        // AppState.activeProfile = props.keep.creator
        Modal.getOrCreateInstance(document.getElementById('keepModal')).hide()
        // logger.log(props.keep.creator.id)
        router.push({name: 'Profile', params: {id: props.keep.creator.id}})
      }
    }
  }
}
</script>
 
 
 <style lang="scss" scoped>
 .modal-content {
   // width: 84vw;
   // height: calc(100vh - 16vw);
   // margin: 8vw;
 }
 
 .img-fluid {
   width: 100%;
 }
 </style>