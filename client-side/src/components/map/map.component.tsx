import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";

import { Container } from "./map.style";

const Map = () => {

  return (
    <Container>
      <MapContainer
        center={{ lat: 	7.28799533, lng: 	5.147500194 }}
        zoom={13}
        scrollWheelZoom={false}>
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      <Marker position={[51.505, -0.09]}>
        <Popup>
          A pretty CSS3 popup. <br /> Easily customizable.
        </Popup>
      </Marker>
    </MapContainer>
    </Container>
  );
};

export default Map;
