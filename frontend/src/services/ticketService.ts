import api from "src/services/api";

export async function getTicketById(ticketId) {
  const response = await api.get(`/ticket/${ticketId}`);
  return response.data;
}

export async function getAllTickets() {
  const response = await api.get(`/ticket`);
  return response.data;
}

export async function updateTicket(ticketId, updatedTicket) {
  const response = await api.put(`/ticket/${ticketId}`, updatedTicket);
  return response.data;
}

export async function createTicket(ticketPayload) {
  const response = await api.post(`/ticket`, ticketPayload);
  return response.data;
}

export async function deleteTicket(ticketId) {
  const response = await api.delete(`/ticket/${ticketId}`);
  return response.data;
}
