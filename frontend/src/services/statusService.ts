import api from "src/services/api";

export async function getAllStatuses() {
  const response = await api.get(`/status`);
  return response.data;
}
