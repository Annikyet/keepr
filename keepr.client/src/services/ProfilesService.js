import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId)
      // logger.log(res.data)
      AppState.activeProfile = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeeps(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/keeps')
      AppState.profileKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaults(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      AppState.profileVaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getMyVaults(profileId) {
    try {
      const res = await api.get('api/profiles/' + profileId + '/vaults')
      AppState.myVaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }
}

export const profilesService = new ProfilesService()