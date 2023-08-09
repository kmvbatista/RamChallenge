import React, { useEffect, useState } from "react";
import Ticket from "../TicketPage/components/Ticket";
import { TicketModel } from "src/models/TicketModel";
import { getAllStatuses } from "src/services/statusApiService";

interface NewTicketPageProps {}

const NewTicketPage: React.FC<NewTicketPageProps> = (props) => {
  const [statuses, setStatuses] = useState<any[]>();
  useEffect(() => {
    async function fetchStatuses() {
      const foundStatuses = await getAllStatuses();
      setStatuses(foundStatuses);
    }
    fetchStatuses();
  }, []);
  return (
    statuses && (
      <Ticket
        ticket={{} as TicketModel}
        statuses={statuses}
        isCreatingNewTicket
      ></Ticket>
    )
  );
};

export default NewTicketPage;
