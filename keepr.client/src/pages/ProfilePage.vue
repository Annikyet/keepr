<template>
  <div class="component">
    <!-- none of this is loading... dafuq? -->
    {{ profile?.name }}
    {{ keeps[0]?.name }}
    {{ vaults[0]?.name }}

  </div>
</template>


<script>
import VaultsList from '../components/VaultsList.vue'
import KeepsList from '../components/KeepsList.vue';
import { profilesService } from '../services/ProfilesService'
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState';
import { logger } from '../utils/Logger.js'
import { useRoute } from 'vue-router'
export default {
    setup() {
      const route = useRoute()
      onMounted(async () => {
        try {
          await profilesService.getProfile(route.params.id)
          await profilesService.getKeeps(route.params.id)
          await profilesService.getVaults(route.params.id)
        } catch (error) {
          logger.error(error)
        }
      })
        return {
          components: { VaultsList, KeepsList },
          profile: computed(() => {AppState.activeProfile}),
          keeps: computed(() => {AppState.profileKeeps}),
          vaults: computed(() => {AppState.profileVaults})
        };
    }
}
</script>


<style lang="scss" scoped>

</style>