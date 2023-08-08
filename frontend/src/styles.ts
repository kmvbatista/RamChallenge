import { Link } from "react-router-dom";
import { Column, Row } from "src/components/GlobalComponents";
import styled from "styled-components";

export const NameInput = styled.input`
  width: 100%;
  height: 12px;
`;

export const OpenTicketButton = styled(Link)`
  font-size: 16px;
`;

export const DescriptionInput = styled.textarea`
  width: 100%;
  height: 40px;
  margin: 8px 0;
`;

export const ImagesList = styled(Row)`
  justify-content: flex-start;
  flex-wrap: wrap;
  max-height: 90px;
  overflow: scroll;
`;

export const TicketImage = styled.img`
  max-width: 80px;
  max-height: 80px;
  padding: 5px;
  padding-left: 0;
`;

export const TicketContainer = styled(Column)`
  width: 300px;
  padding: 5px;
  border-radius: 5px;
  box-shadow: #e3e2e245 1px 1px 10px 0px;
  margin: 7px 10px;
  background: #b6b6b6;
`;

export const BoardTitle = styled.p`
  text-align: center;
`;

export const FavoriteIcon = styled.img`
  width: 14px;
  height: 14px;
  margin-right: 5px;
`;
