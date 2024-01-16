import { Button, Container, Navbar, Row } from "react-bootstrap";
import Item from "./Components/Item";

function App() {
  return (
    <>
      <Navbar expand="lg" className="bg-body-tertiary mb-5">
        <Container>
          <Navbar.Brand>
            <a href="/dashboard">Todo</a>
          </Navbar.Brand>
          <Button>
            <a href="/add">Add Todo</a>
          </Button>{" "}
        </Container>
      </Navbar>

      <Container>
        <Row className="mb-4">
          <Item />
        </Row>
      </Container>
    </>
  );
}

export default App;
