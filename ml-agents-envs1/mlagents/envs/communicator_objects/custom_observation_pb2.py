# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: mlagents/envs/communicator_objects/custom_observation.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='mlagents/envs/communicator_objects/custom_observation.proto',
  package='communicator_objects',
  syntax='proto3',
  serialized_options=_b('\252\002\034MLAgents.CommunicatorObjects'),
  serialized_pb=_b('\n;mlagents/envs/communicator_objects/custom_observation.proto\x12\x14\x63ommunicator_objects\"\x18\n\x16\x43ustomObservationProtoB\x1f\xaa\x02\x1cMLAgents.CommunicatorObjectsb\x06proto3')
)




_CUSTOMOBSERVATIONPROTO = _descriptor.Descriptor(
  name='CustomObservationProto',
  full_name='communicator_objects.CustomObservationProto',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=85,
  serialized_end=109,
)

DESCRIPTOR.message_types_by_name['CustomObservationProto'] = _CUSTOMOBSERVATIONPROTO
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

CustomObservationProto = _reflection.GeneratedProtocolMessageType('CustomObservationProto', (_message.Message,), {
  'DESCRIPTOR' : _CUSTOMOBSERVATIONPROTO,
  '__module__' : 'mlagents.envs.communicator_objects.custom_observation_pb2'
  # @@protoc_insertion_point(class_scope:communicator_objects.CustomObservationProto)
  })
_sym_db.RegisterMessage(CustomObservationProto)


DESCRIPTOR._options = None
# @@protoc_insertion_point(module_scope)
