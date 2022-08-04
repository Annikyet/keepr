import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAll() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async create(newKeep) {
    try {
      const res = await api.post('api/keeps', newKeep)
      logger.log(res.data)
    } catch (error) {
      logger.error(error)
    }
  }
}

export const keepsService = new KeepsService()