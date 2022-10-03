import React from 'react';
import { Container } from 'semantic-ui-react';
import { NavBar } from './NavBar';
import { ActivityDashboard } from '../../features/activities/dashboard/ActivitiesDashboard';
import { observer } from 'mobx-react-lite';
import { Route, Routes, useLocation } from 'react-router-dom';
import Home from '../../features/home/Home';
import { ActivityForm } from '../../features/activities/form/ActivityForm';
import { ActivityDetails } from '../../features/activities/details/ActivityDetails';

function App() {
  const location = useLocation();

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/activities" element={<ActivityDashboard />} />
          <Route path="/activities/:id" element={<ActivityDetails />} />
          <Route key={location.key} path="/activities/new" element={<ActivityForm />} />
          <Route key={location.key} path="activities/edit/:id" element={<ActivityForm />} />
        </Routes>
      </Container>
    </>
  );
}

export default observer(App);
