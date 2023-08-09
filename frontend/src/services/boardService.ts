import api from "./api";

export async function createBoard(boardPayload) {
  const response = await api.post(`/board`, boardPayload);
  return response.data;
}
