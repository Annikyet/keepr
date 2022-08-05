import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'

class VaultKeepsService {
  async create(keepId, vaultId) {
      let newVaultKeep = {
        vaultId: vaultId,
        keepId: keepId
      }
      const res = await api.post('api/vaultkeeps', newVaultKeep)
      AppState.vaultKeeps.push(res.data)
  }

  async delete(vaultKeepId) {
      logger.log(vaultKeepId)
      await api.delete('api/vaultkeeps/' + vaultKeepId)
      await vaultsService.getVaultKeeps(AppState.activeVault.id)
  }
}

export const vaultKeepsService = new VaultKeepsService()