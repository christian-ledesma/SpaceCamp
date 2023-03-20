import { useEffect } from "react";
import { Container } from "semantic-ui-react";
import { NavBar } from "./NavBar";
import { observer } from "mobx-react-lite";
import { Outlet, ScrollRestoration, useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { useStore } from "../stores/store";
import { Loading } from "./Loading";
import { ModalContainer } from "../common/modals/ModalContainer";
import Home from "../../features/home/Home";

function App() {
  const location = useLocation();
  const { commonStore, userStore } = useStore();

  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore]);

  if (!commonStore.appLoaded) return <Loading />;

  return (
    <>
      <ScrollRestoration />
      <ToastContainer position="bottom-right" />
      <ModalContainer />
      {location.pathname === "/" ? (
        <Home />
      ) : (
        <>
          <NavBar />
          <Container style={{ marginTop: "7em" }}>
            <Outlet />
          </Container>
        </>
      )}
    </>
  );
}

export default observer(App);
