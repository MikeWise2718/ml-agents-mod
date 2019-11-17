# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: mlagents/envs/communicator_objects/unity_rl_initialization_output.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()


from mlagents.envs.communicator_objects import brain_parameters_pb2 as mlagents_dot_envs_dot_communicator__objects_dot_brain__parameters__pb2
from mlagents.envs.communicator_objects import environment_parameters_pb2 as mlagents_dot_envs_dot_communicator__objects_dot_environment__parameters__pb2


DESCRIPTOR = _descriptor.FileDescriptor(
  name='mlagents/envs/communicator_objects/unity_rl_initialization_output.proto',
  package='communicator_objects',
  syntax='proto3',
  serialized_options=_b('\252\002\034MLAgents.CommunicatorObjects'),
  serialized_pb=_b('\nGmlagents/envs/communicator_objects/unity_rl_initialization_output.proto\x12\x14\x63ommunicator_objects\x1a\x39mlagents/envs/communicator_objects/brain_parameters.proto\x1a?mlagents/envs/communicator_objects/environment_parameters.proto\"\xeb\x01\n UnityRLInitializationOutputProto\x12\x0c\n\x04name\x18\x01 \x01(\t\x12\x0f\n\x07version\x18\x02 \x01(\t\x12\x10\n\x08log_path\x18\x03 \x01(\t\x12\x44\n\x10\x62rain_parameters\x18\x05 \x03(\x0b\x32*.communicator_objects.BrainParametersProto\x12P\n\x16\x65nvironment_parameters\x18\x06 \x01(\x0b\x32\x30.communicator_objects.EnvironmentParametersProtoB\x1f\xaa\x02\x1cMLAgents.CommunicatorObjectsb\x06proto3')
  ,
  dependencies=[mlagents_dot_envs_dot_communicator__objects_dot_brain__parameters__pb2.DESCRIPTOR,mlagents_dot_envs_dot_communicator__objects_dot_environment__parameters__pb2.DESCRIPTOR,])




_UNITYRLINITIALIZATIONOUTPUTPROTO = _descriptor.Descriptor(
  name='UnityRLInitializationOutputProto',
  full_name='communicator_objects.UnityRLInitializationOutputProto',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='name', full_name='communicator_objects.UnityRLInitializationOutputProto.name', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='version', full_name='communicator_objects.UnityRLInitializationOutputProto.version', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='log_path', full_name='communicator_objects.UnityRLInitializationOutputProto.log_path', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='brain_parameters', full_name='communicator_objects.UnityRLInitializationOutputProto.brain_parameters', index=3,
      number=5, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='environment_parameters', full_name='communicator_objects.UnityRLInitializationOutputProto.environment_parameters', index=4,
      number=6, type=11, cpp_type=10, label=1,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
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
  serialized_start=222,
  serialized_end=457,
)

_UNITYRLINITIALIZATIONOUTPUTPROTO.fields_by_name['brain_parameters'].message_type = mlagents_dot_envs_dot_communicator__objects_dot_brain__parameters__pb2._BRAINPARAMETERSPROTO
_UNITYRLINITIALIZATIONOUTPUTPROTO.fields_by_name['environment_parameters'].message_type = mlagents_dot_envs_dot_communicator__objects_dot_environment__parameters__pb2._ENVIRONMENTPARAMETERSPROTO
DESCRIPTOR.message_types_by_name['UnityRLInitializationOutputProto'] = _UNITYRLINITIALIZATIONOUTPUTPROTO
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

UnityRLInitializationOutputProto = _reflection.GeneratedProtocolMessageType('UnityRLInitializationOutputProto', (_message.Message,), {
  'DESCRIPTOR' : _UNITYRLINITIALIZATIONOUTPUTPROTO,
  '__module__' : 'mlagents.envs.communicator_objects.unity_rl_initialization_output_pb2'
  # @@protoc_insertion_point(class_scope:communicator_objects.UnityRLInitializationOutputProto)
  })
_sym_db.RegisterMessage(UnityRLInitializationOutputProto)


DESCRIPTOR._options = None
# @@protoc_insertion_point(module_scope)
