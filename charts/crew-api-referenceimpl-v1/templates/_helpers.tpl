{{- /*
Expand the name of the chart.
*/ -}}
{{- define "crew-api-referenceimpl-v1.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- /*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/ -}}
{{- define "crew-api-referenceimpl-v1.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}


{{- /*
crew-api-referenceimpl-v1.labels.standard prints the standard crew-api-referenceimpl-v1 Helm labels.

The standard labels are frequently used in metadata.
*/ -}}
{{- define "crew-api-referenceimpl-v1.labels.standard" -}}
app: {{ template "crew-api-referenceimpl-v1.name" . }}
chart: {{ template "crew-api-referenceimpl-v1.chartref" . }}
heritage: {{ .Release.Service | quote }}
release: {{ .Release.Name | quote }}
{{- end -}}

{{- /*
crew-api-referenceimpl-v1.chartref prints a chart name and version.

It does minimal escaping for use in Kubernetes labels.

Example output:

crew-api-referenceimpl-v1-0.4.5
*/ -}}
{{- define "crew-api-referenceimpl-v1.chartref" -}}
{{- replace "+" "_" .Chart.Version | printf "%s-%s" .Chart.Name -}}
{{- end -}}