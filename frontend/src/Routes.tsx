import React from "react";
import { Route, Routes } from "react-router-dom";
import Board from "./pages/BoardPage";
import TicketPage from "./pages/TicketPage";
import NewTicketPage from "./pages/NewTicketPage";
import NewStatus from "./pages/BoardPage/components/NewStatus";

interface RoutesProps {}

const AppRoutes: React.FC<RoutesProps> = (props) => {
  return (
    <Routes>
      <Route path="/" element={<Board></Board>}></Route>
      <Route path="/status/new" element={<NewStatus></NewStatus>}></Route>
      <Route
        path="/ticket/new"
        element={<NewTicketPage></NewTicketPage>}
      ></Route>
      <Route path="/ticket/:id" element={<TicketPage></TicketPage>}></Route>
    </Routes>
  );
};

export default AppRoutes;
