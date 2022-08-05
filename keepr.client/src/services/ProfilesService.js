import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(profileId) {
      const res = await api.get('api/profiles/' + profileId)
      // logger.log(res.data)
      AppState.activeProfile = res.data
  }

  async getKeeps(profileId) {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      AppState.profileKeeps = res.data
  }

  async getVaults(profileId) {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      logger.log(res.data)
      AppState.profileVaults = res.data
  }

  async getMyVaults(profileId) {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      AppState.myVaults = res.data
  }
}

export const profilesService = new ProfilesService()