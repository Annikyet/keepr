import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'
import { profilesService } from './ProfilesService'
import { vaultKeepsService } from './VaultKeepsService'

class VaultsService {
  async getVault(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }

  async getVaultKeeps(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }

  async createVault(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      // logger.log(res.data)
      // AppState.myVaults.push(res.data)
      profilesService.getVaults(AppState.account.id)
      AppState.myVaults = AppState.profileVaults
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }

  async delete(vaultId, vaultKeeps) {
    try {
      // vaultKeeps will collect in db... if this isn't here
      // for (let vk = 0; vk < vaultKeeps.length; vk++) {
      //   await vaultKeepsService.delete(vaultKeeps[vk].id)
      // }
      await api.delete('api/vaults/' + vaultId)
      profilesService.getVaults(AppState.account.id)
      AppState.myVaults = AppState.profileVaults
    } catch (error) {
      logger.error(error)
      Pop.error(error)
    }
  }
}

export const vaultsService = new VaultsService()