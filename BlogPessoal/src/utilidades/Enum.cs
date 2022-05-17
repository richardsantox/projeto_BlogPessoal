﻿using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilidades
{
    /// <summary>
    /// <para>Resumo: Enum responsável por definir Tipos de usuário do sistema</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }
}
