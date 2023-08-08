import React, { useEffect, useState } from "react";
import Ticket from "../TicketPage/components/Ticket";
import { TicketModel } from "src/models/TicketModel";
import { getAllStatuses } from "src/services/statusService";

interface NewTicketPageProps {}

const NewTicketPage: React.FC<NewTicketPageProps> = (props) => {
  debugger;
  const [statuses, setStatuses] = useState<any[]>();
  useEffect(() => {
    async function fetchStatuses() {
      const foundStatuses = await getAllStatuses();
      debugger;
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
