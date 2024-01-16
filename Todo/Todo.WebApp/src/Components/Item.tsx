import React from "react";
import {
  Button,
  Col,
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  CardText,
} from "react-bootstrap";

interface Props {
  title: string;
  description: string;
  category: number;
  date: string;
}

const Item: React.FC<Props> = ({
  title,
  description,
  category,
  date,
}: Props): JSX.Element => {
  return (
    <Col>
      <Card border="primary">
        <CardHeader className="d-flex justify-content-between">
          {category}
          <div>
            <Button variant="primary">Edit</Button>{" "}
            <Button variant="success">Complete</Button>{" "}
            <Button variant="danger">Delete</Button>{" "}
          </div>
        </CardHeader>
        <CardBody>
          <CardTitle>{title}</CardTitle>
          <CardText>{description}</CardText>
          <p className="mb-0">
            <i className="bi bi-calendar"></i>
            Due date {date}
          </p>
        </CardBody>
      </Card>
    </Col>
  );
};

export default Item;
