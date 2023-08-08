import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Ticket from "./components/Ticket";
import { TicketModel } from "src/models/TicketModel";
import { getTicketById } from "src/services/ticketService";
import { getAllStatuses } from "src/services/statusService";

interface TicketPageProps {}

const TicketPage: React.FC<TicketPageProps> = (props) => {
  const [ticket, setTicket] = useState<TicketModel>();
  const [statuses, setStatuses] = useState<any[]>();
  const { id } = useParams();
  useEffect(() => {
    async function fetchTicket() {
      const foundTicket = await getTicketById(id);
      setTicket(foundTicket);
    }
    async function fetchStatuses() {
      const foundStatuses = await getAllStatuses();
      setStatuses(foundStatuses);
    }
    fetchStatuses();
    fetchTicket();
  }, [id]);
  if (!ticket) {
    return null;
  }
  return <Ticket ticket={ticket} statuses={statuses}></Ticket>;
};

export default TicketPage;
