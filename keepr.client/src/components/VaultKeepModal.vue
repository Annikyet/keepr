 <template>
  <div class="component">
    <div class="modal fade" id="vaultKeepModal" tabindex="-1" aria-labelledby="vaultKeepModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content border-0">
          <div class="card border-0">
            <div class="row">
              <div class="col-6">
                <img :src="keep?.img" :alt="keep?.name" class="img-fluid">
              </div>
              <div class="col-6">
                <div class="card-body h-100">
                  <div class="d-flex flex-column justify-content-between h-100">
                    <div>
                      <div class="d-flex justify-content-end">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>
                      <KeepStatsBar :keep="keep" />
                      <h5 class=" py-4 card-title text-center">{{ keep?.name }}</h5>
                      <div class="">
                        <p class="card-text">{{ keep?.description }}</p>
                      </div>
                      <hr>
                      <!-- tags go here -->
                    </div>
                    <div class="d-flex justify-content-between align-items-end">

                      <button class="btn btn-outline-primary">add to vault</button>
                      <!-- account is empty... -->
                      <button v-show="account?.id == keep?.creator?.id" class="btn btn-primary">trashcan</button>
                      <div @click="viewProfile(keep?.creator)" class="d-flex align-items-end">
                        <img :src="keep?.creator?.picture" :alt="keep?.creator?.name"
                          class="profile-pic rounded-3 me-2">
                        <p class="mb-0 profile-text">{{ keep?.creator?.name }}</p>
                      </div>
                    </div>
                  </div>
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
import KeepStatsBar from './KeepStatsBar.vue'
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
    const router = useRouter();
    return {
      account: computed(() => { AppState.account; }),
      viewProfile() {
        // console.log(user.name)
        // AppState.activeProfile = props.keep.creator
        Modal.getOrCreateInstance(document.getElementById("vaultKeepModal")).hide();
        // logger.log(props.keep.creator.id)
        router.push({ name: "Profile", params: { id: props.keep.creator.id } });
      }
    };
  },
  components: { KeepStatsBar }
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
 
 .card-title {
   font-size: 32px;
   font-weight: 300;
 }
 
 .profile-pic {
   height: 36px;
   width: 36px;
   object-fit: contain;
 }
 
 .profile-text {
   font-size: 18px;
   font-weight: 300;
 }
 </style>