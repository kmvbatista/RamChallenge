import api from "src/services/api";

export async function getAllStatuses() {
  const response = await api.get(`/status`);
  return response.data;
}
export async function createStatus(statusPayload) {
  const response = await api.post(`/status`, statusPayload);
  return response.data;
}
