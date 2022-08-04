 <template>
  <div class="component">
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-xl modal-dialog-centered">
        <div class="modal-content border-0 p-2">
          <div class="card border-0">
            <div class="row">
              <div class="col-6">
                <img :src="keep?.img" :alt="keep?.name" class="img-fluid rounded-3">
              </div>
              <div class="col-6">
                <div class="card-body h-100 p-0">
                  <div class="d-flex flex-column justify-content-between h-100">
                    <div>
                      <div class="d-flex justify-content-end">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>
                      <KeepStatsBar :keep="keep" />
                      <h5 class=" py-4 mx-5 card-title text-center">{{ keep?.name }}</h5>
                      <div class="">
                        <p class="card-text mx-5">{{ keep?.description }}</p>
                      </div>
                      <hr class="mx-5">
                      <!-- tags go here -->
                    </div>
                    <slot></slot>
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
        Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
        // logger.log(props.keep.creator.id)
        router.push({ name: "Profile", params: { id: props.keep.creator.id } });
      }
    };
  },
  components: { KeepStatsBar }
}
</script>
 
 
 <style lang="scss" scoped>
 
 .img-fluid {
   width: 100%;
 }
 
 .card-title {
   font-size: 32px;
   font-weight: 300;
 }
 </style>