<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:eml="eml://ecoinformatics.org/eml-2.1.1" version="1.0">
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
    <html>
        <head>
            <link rel="stylesheet" href="styles.css" type="text/css"/>
            <title>Meta Data</title>
        </head>
        <body>
            <xsl:apply-templates/>
        </body>
    </html>
    </xsl:template>

    <xsl:template match="eml:eml">
        <h2>
        <xsl:value-of select="dataset/title"/>
        </h2>
        <h4>Abstract</h4>
        <xsl:for-each select="dataset/abstract/para">
            <p><xsl:value-of select="."/></p>
        </xsl:for-each>
        <h4>Citation</h4>
        <xsl:value-of select="additionalMetadata/metadata/gbif/citation"/>
    </xsl:template>

</xsl:stylesheet>